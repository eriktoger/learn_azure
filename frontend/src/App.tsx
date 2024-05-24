import { useEffect, useState } from "react";
import { setupAppInsights } from "./setupAppInsights";

function App() {
  const [fromBackend, setFromBackend] = useState("Loading...");
  const [fromDatabase, setFromDatabase] = useState("Nothing from DB");
  const [fromDocker, setFromDocker] = useState("Nothing from docker");
  const [fromFunction, setFromFunction] = useState("Nothing from function");
  const [name, setName] = useState("Erik");
  const url = import.meta.env.VITE_BACKEND_URL;
  const appInsights = setupAppInsights();

  useEffect(() => {
    if (!url) {
      console.log(import.meta.env);
      setFromBackend("No url found.");
      return;
    }
    fetch(`${url}/helloworld`, {
      mode: "cors",
      method: "GET",
      headers: {
        "Access-Control-Allow-Origin": "*",
      },
    })
      .then((response) => {
        return response.text();
      })
      .then((text) => {
        setFromBackend(text);
      })
      .catch(() => {
        setFromBackend("Backend failed, refresh to try again.");
      });
  }, [url]);

  const onCounterClick = async () => {
    appInsights.trackEvent({
      name: "GetCounter",
      properties: { message: "Frontend is requesting counter from backend" },
    });
    try {
      const response = await fetch(`${url}/counter`, {
        mode: "cors",
        method: "GET",
        headers: {
          "Access-Control-Allow-Origin": "*",
        },
      });
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
      const response = await fetch(`${url}/docker`, {
        mode: "cors",
        method: "GET",
        headers: {
          "Access-Control-Allow-Origin": "*",
        },
      });

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
      const response = await fetch(`${url}/function?name=${name}`, {
        mode: "cors",
        method: "GET",
        headers: {
          "Access-Control-Allow-Origin": "*",
        },
      });

      const text = await response.text();

      setFromFunction(text);
    } catch (error) {
      setFromFunction("Function call failed...");
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
    </div>
  );
}

export default App;
