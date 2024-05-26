import { useCallback, useEffect, useState } from "react";
import { setupAppInsights } from "./setupAppInsights";

function App() {
  const [fromBackend, setFromBackend] = useState("Loading...");
  const [fromDatabase, setFromDatabase] = useState("Nothing from DB");
  const [fromDocker, setFromDocker] = useState("Nothing from docker");
  const [fromFunction, setFromFunction] = useState("Nothing from function");
  const [fromRedis, setFromRedis] = useState("Nothing from redis");
  const [name, setName] = useState("Erik");
  const url = import.meta.env.VITE_BACKEND_URL;
  const appInsights = setupAppInsights();

  const backendFetch = useCallback(
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

  useEffect(() => {
    if (!url) {
      console.log(import.meta.env);
      setFromBackend("No url found.");
      return;
    }
    backendFetch("helloWorld")
      .then((response) => {
        return response.text();
      })
      .then((text) => {
        setFromBackend(text);
      })
      .catch(() => {
        setFromBackend("Backend failed, refresh to try again.");
      });
  }, [backendFetch, url]);

  const onCounterClick = async () => {
    appInsights.trackEvent({
      name: "GetCounter",
      properties: { message: "Frontend is requesting counter from backend" },
    });
    try {
      const response = await backendFetch("counter");
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
      const response = await backendFetch("docker");
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
      const response = await backendFetch(`function?name=${name}`);
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
      const response = await backendFetch(`redis`);

      const text = await response.text();

      setFromRedis(text);
    } catch (error) {
      setFromRedis("Redis call failed...");
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
    </div>
  );
}

export default App;
