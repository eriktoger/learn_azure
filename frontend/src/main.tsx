import React, { useEffect, useState } from "react";
import ReactDOM from "react-dom/client";
import App from "./App.tsx";
import "./index.css";
import { Configuration, PublicClientApplication } from "@azure/msal-browser";
import { MsalProvider } from "@azure/msal-react";

const configuration: Configuration = {
  auth: {
    clientId: import.meta.env.VITE_MSAL_CLIENT_ID,
    authority: import.meta.env.VITE_MSAL_AUTHORITY,
  },
};

const msalInstance = new PublicClientApplication(configuration);

const AppWithMsal = () => {
  const [isMsalLoading, setIsMsalLoading] = useState(true);

  useEffect(() => {
    msalInstance.initialize().then(() => setIsMsalLoading(false));
  }, []);

  if (isMsalLoading) {
    return <span>Loading...</span>;
  }

  return (
    <MsalProvider instance={msalInstance}>
      <App />
    </MsalProvider>
  );
};

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <AppWithMsal />
  </React.StrictMode>
);
