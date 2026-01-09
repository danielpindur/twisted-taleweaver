import { ClerkProvider, SignedIn, SignedOut } from "@clerk/clerk-react";
import { BrowserRouter, Routes, Route } from 'react-router'
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { ReactQueryDevtools } from '@tanstack/react-query-devtools'
import { TooltipProvider } from "./components/ui/tooltip";
import { Toaster } from "@/components/ui/toaster";
import { Toaster as Sonner } from "@/components/ui/sonner";
import { ProtectedRoute } from "@/components/auth/ProtectedRoute";
import Landing from "@/pages/Landing";
import NotFound from "@/pages/NotFound";
import Dashboard from "@/pages/Dashboard";
import Expedition from "@/pages/Expedition";
import { WebSocketClientProvider } from "./components/websocket/WebSocketClientProvider";
import Archives from "./pages/Archives";

const PUBLISHABLE_KEY = import.meta.env.VITE_CLERK_PUBLISHABLE_KEY;
if (!PUBLISHABLE_KEY) {
  throw new Error("Missing Clerk Publishable Key");
}

const queryClient = new QueryClient();

function App() {
	return (
		<QueryClientProvider client={queryClient}>
			<TooltipProvider>
				<ClerkProvider publishableKey={PUBLISHABLE_KEY}>
					<WebSocketClientProvider>
						<BrowserRouter>
							<Routes>
								{/* Public routes */}
								<Route path="/" element={
									<>
										<SignedIn>
											<Dashboard />
										</SignedIn>
										<SignedOut>
											<Landing />
										</SignedOut>
									</>} />

								{/* Protected routes */}
								<Route 
                  path="/expedition/:expeditionId" 
                  element={
                  <ProtectedRoute>
                    <Expedition />
                  </ProtectedRoute>
                  } 
                />

                <Route 
                  path="/archives" 
                  element={
                    <ProtectedRoute>
                      <Archives />
                    </ProtectedRoute>
                  } 
                />

								{/* 404 route */}
								<Route path="*" element={<NotFound />} />
							</Routes>
						</BrowserRouter>
						<Toaster />
						<Sonner />
					</WebSocketClientProvider>
				</ClerkProvider>
			</TooltipProvider>
			<ReactQueryDevtools initialIsOpen={false} />
		</QueryClientProvider>
	);
}

export default App;