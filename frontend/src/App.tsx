import { useCallback, useEffect, useState } from "react";
import { setupAppInsights } from "./setupAppInsights";

function App() {
  const [fromBackend, setFromBackend] = useState("Loading...");
  const [fromDatabase, setFromDatabase] = useState("Nothing from DB");
  const [fromDocker, setFromDocker] = useState("Nothing from docker");
  const [fromFunction, setFromFunction] = useState("Nothing from function");
  const [fromRedis, setFromRedis] = useState("Nothing from redis");
  const [fromQueue, setFromQueue] = useState("Nothing from queue");
  const [fromQueueLength, setFromQueueLength] = useState(
    "Nothing from queue length"
  );
  const [name, setName] = useState("Erik");
  const [message, setMessage] = useState("Queue message");
  const url = import.meta.env.VITE_BACKEND_URL;
  const appInsights = setupAppInsights();

  const backendGet = useCallback(
    async (urlSuffix: string) => {
      return await fetch(`${url}/${urlSuffix}`, {
        mode: "cors",
        method: "GET",

        headers: {
          "Access-Control-Allow-Origin": "*",
        },
      });
    },
    [url]
  );

  const backendPost = useCallback(
    async (urlSuffix: string, body: object) => {
      return await fetch(`${url}/${urlSuffix}`, {
        mode: "cors",
        method: "POST",
        body: JSON.stringify(body),
        headers: {
          "Content-Type": "application/json",
          "Access-Control-Allow-Origin": "*",
        },
      });
    },
    [url]
  );

  useEffect(() => {
    if (!url) {
      console.log(import.meta.env);
      setFromBackend("No url found.");
      return;
    }
    backendGet("helloWorld")
      .then((response) => {
        return response.text();
      })
      .then((text) => {
        setFromBackend(text);
      })
      .catch(() => {
        setFromBackend("Backend failed, refresh to try again.");
      });
  }, [backendGet, url]);

  const onCounterClick = async () => {
    appInsights.trackEvent({
      name: "GetCounter",
      properties: { message: "Frontend is requesting counter from backend" },
    });
    try {
      const response = await backendGet("counter");
      const text = await response.text();
      setFromDatabase(text);
    } catch (error) {
      setFromDatabase("Datbase call failed...");
    }
  };

  const onDockerClick = async () => {
    appInsights.trackEvent({
      name: "GetDocker",
      properties: { message: "Frontend is requesting docker from backend" },
    });
    try {
      const response = await backendGet("docker");
      const text = await response.text();

      setFromDocker(text);
    } catch (error) {
      setFromDocker("Docker has been shut down to save money");
    }
  };

  const onFunctionClick = async () => {
    appInsights.trackEvent({
      name: "GetTriggerFunction",
      properties: { message: "Frontend is requesting an azure function call" },
    });

    try {
      const response = await backendGet(`function?name=${name}`);
      const text = await response.text();

      setFromFunction(text);
    } catch (error) {
      setFromFunction("Function call failed...");
    }
  };

  const onRedisClick = async () => {
    appInsights.trackEvent({
      name: "GetRedis",
      properties: { message: "Frontend is requesting an azure redis call" },
    });

    try {
      const response = await backendGet(`redis`);

      const text = await response.text();

      setFromRedis(text);
    } catch (error) {
      setFromRedis("Redis has been shut down to save money");
    }
  };

  const onQueueGet = async (peek: boolean) => {
    appInsights.trackEvent({
      name: "GetQueue",
      properties: { message: "Frontend is requesting an azure queue call" },
    });

    try {
      const response = await backendGet(peek ? "queue?peek=true" : "queue");

      const text = await response.text();

      setFromQueue(text);
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
    } catch (error: any) {
      if (error.status === 404) {
        setFromQueue("Queue is empty");
      } else {
        setFromQueue("Queue call failed...");
      }
    }
  };

  const onQueuePost = async () => {
    appInsights.trackEvent({
      name: "GetRedis",
      properties: { message: "Frontend is requesting an azure redis call" },
    });

    try {
      await backendPost(`queue`, { content: message });
    } catch (error) {
      console.warn(error);
    }
  };

  const onQueueLengthGet = async () => {
    appInsights.trackEvent({
      name: "GetQueue/queue-length",
      properties: {
        message: "Frontend is requesting an azure queue length call",
      },
    });

    try {
      const response = await backendGet("queue/length");

      const text = await response.text();

      setFromQueueLength(text);
    } catch (error) {
      setFromQueueLength("Queue length call failed...");
    }
  };

  return (
    <div>
      <p>Hello, World! (from Frontend)</p>
      <p>{fromBackend}</p>
      <button onClick={onCounterClick}>Click me to connect to database</button>
      <p>{fromDatabase}</p>

      <button onClick={onDockerClick}>Click me to connect to docker</button>
      <p>{fromDocker}</p>

      <input
        value={name}
        onChange={(event) => setName(event.currentTarget.value)}
      />
      <button onClick={onFunctionClick}>Click me to connect to function</button>
      <p>{fromFunction}</p>

      <p>Image from blob storage</p>
      <p>{<img src={`${url}/file?filename=cats.jfif`} />}</p>
      <button onClick={onRedisClick}>Click me to connect to redis</button>
      <p>The value is cached for 5 minutes</p>
      <p>{fromRedis}</p>

      <input
        value={message}
        onChange={(event) => setMessage(event.currentTarget.value)}
        style={{ width: 150 }}
      />
      <div style={{ display: "flex", flexDirection: "column", width: 158 }}>
        <button onClick={onQueuePost}>Send to queue</button>
        <button onClick={() => onQueueGet(true)}>Peek</button>
        <button onClick={() => onQueueGet(false)}>Consume</button>
        <button onClick={onQueueLengthGet}>Queue Length</button>
      </div>
      <p>{fromQueue}</p>
      <p>Queue Length: {fromQueueLength}</p>
    </div>
  );
}

export default App;
