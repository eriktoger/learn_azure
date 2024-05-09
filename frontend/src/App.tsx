import { useEffect, useState } from "react";
import { setupAppInsights } from "./setupAppInsights";

function App() {
  const [fromBackend, setFromBackend] = useState("Loading...");
  const [fromDatabase, setFromDatabase] = useState("Nothing from DB");
  const [fromDocker, setFromDocker] = useState("Nothing from docker");
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
      setFromDocker("Docker call failed...");
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
    </div>
  );
}

export default App;
