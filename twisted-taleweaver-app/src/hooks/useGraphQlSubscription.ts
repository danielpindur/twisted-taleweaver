import { useEffect, useMemo, useRef } from "react";
import type { DocumentNode } from "graphql";
import { print } from "graphql";
import { useAuth } from "@clerk/clerk-react";
import { useWebSocketClient } from "@/components/websocket/WebSocketClientProvider";

interface SubscriptionOptions<T> {
  document: DocumentNode;
  variables?: Record<string, any>;
  onNext: (data: T) => void;
  onError?: (error: unknown) => void;
  onComplete?: () => void;
}

export function useGraphQlSubscription<T>({
  document,
  variables = {},
  onNext,
  onError,
  onComplete,
}: SubscriptionOptions<T>) {
  const { getToken, sessionId } = useAuth();
  const { connect } = useWebSocketClient();

  const stableVariables = useMemo(() => variables, [JSON.stringify(variables)]);

  const onNextRef = useRef(onNext);
  const onErrorRef = useRef(onError);
  const onCompleteRef = useRef(onComplete);

  useEffect(() => {
    onNextRef.current = onNext;
    onErrorRef.current = onError;
    onCompleteRef.current = onComplete;
  });

  useEffect(() => {
    let disposed = false;
    let unsubscribe: (() => void) | undefined;

    const start = async () => {
      const token = await getToken();
      if (!token || disposed) return;

      const client = await connect(token);
      if (disposed) return;

      unsubscribe = client.subscribe(
        {
          query: print(document),
          variables: stableVariables,
        },
        {
          next: (result: any) => {
            if (disposed) return;

            if (result.errors) {
              onErrorRef.current?.(result.errors);
              return;
            }

            if (result.data) {
              onNextRef.current(result.data as T);
            }
          },
          error: (err) => {
            if (!disposed) {
              onErrorRef.current?.(err);
            }
          },
          complete: () => {
            onCompleteRef.current?.();
          },
        }
      );
    };

    start();

    return () => {
      disposed = true;
      unsubscribe?.();
    };
  }, [document, stableVariables, connect, getToken, sessionId]);
}
