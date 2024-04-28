import { useEffect, useState } from "react";

function App() {
  const [fromBackend, setFromBackend] = useState("Loading...");
  useEffect(() => {
    const url = import.meta.env.VITE_BACKEND_URL;
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
  }, []);
  return (
    <div>
      <p>Hello, World! (from Frontend)</p>
      <p>{fromBackend}</p>
    </div>
  );
}

export default App;
