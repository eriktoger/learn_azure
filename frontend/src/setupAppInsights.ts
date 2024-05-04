import { ApplicationInsights } from "@microsoft/applicationinsights-web";

export const setupAppInsights = () => {
  const instrumentationKey = import.meta.env
    .VITE_APPLICATIONINSIGHTS_INSTRUMENTATIONKEY;
  const appInsights = new ApplicationInsights({
    config: {
      instrumentationKey,
      enableAutoRouteTracking: true,
    },
  });

  appInsights.loadAppInsights();
  return appInsights;
};
