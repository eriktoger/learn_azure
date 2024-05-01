import { useEffect, useState } from "react";

function App() {
  const [fromBackend, setFromBackend] = useState("Loading...");
  const [fromDatabase, setFromDatabase] = useState("Nothing from DB");
  const url = import.meta.env.VITE_BACKEND_URL;

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

  const onClick = async () => {
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

  return (
    <div>
      <p>Hello, World! (from Frontend)</p>
      <p>{fromBackend}</p>
      <button onClick={onClick}>Click me to connect to database</button>
      <p>{fromDatabase}</p>
    </div>
  );
}

export default App;
