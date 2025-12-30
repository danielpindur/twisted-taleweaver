import React, {
  createContext,
  useContext,
  useEffect,
  useRef,
} from "react";
import { createClient, type Client } from "graphql-ws";

interface WebSocketClientContextValue {
  getClient: () => Client | null;
  connect: (token: string) => Promise<Client>;
  disconnect: () => void;
}

const WebSocketClientContext =
  createContext<WebSocketClientContextValue | undefined>(undefined);

export const WebSocketClientProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const clientRef = useRef<Client | null>(null);
  const connectPromiseRef = useRef<Promise<Client> | null>(null);

  const connect = (token: string): Promise<Client> => {
    // already connected
    if (clientRef.current) {
      return Promise.resolve(clientRef.current);
    }

    // connection in progress
    if (connectPromiseRef.current) {
      return connectPromiseRef.current;
    }

    connectPromiseRef.current = new Promise<Client>((resolve, reject) => {
      const client = createClient({
        url: "ws://localhost:8081/graphql",
        connectionParams: {
          Authorization: `Bearer ${token}`,
        },
        lazy: false,
        on: {
          connected: () => {
            clientRef.current = client;
            resolve(client);
          },
          closed: () => {
            clientRef.current = null;
            connectPromiseRef.current = null;
          },
          error: reject,
        },
      });
    });

    return connectPromiseRef.current;
  };

  const disconnect = () => {
    clientRef.current?.dispose();
    clientRef.current = null;
    connectPromiseRef.current = null;
  };

  useEffect(() => disconnect, []);

  return (
    <WebSocketClientContext.Provider
      value={{ getClient: () => clientRef.current, connect, disconnect }}
    >
      {children}
    </WebSocketClientContext.Provider>
  );
};

export const useWebSocketClient = () => {
  const ctx = useContext(WebSocketClientContext);
  if (!ctx) {
    throw new Error(
      "useWebSocketClient must be used within WebSocketClientProvider"
    );
  }
  return ctx;
};
