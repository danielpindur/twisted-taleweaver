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

							{/* Protected routes 
							<Route 
                path="/rooms" 
                element={
                  <ProtectedRoute>
                    <Rooms />
                  </ProtectedRoute>
                } 
              />*/}

							{/* 404 route */}
							<Route path="*" element={<NotFound />} />
						</Routes>
					</BrowserRouter>
					<Toaster />
					<Sonner />
				</ClerkProvider>
			</TooltipProvider>
			<ReactQueryDevtools initialIsOpen={false} />
		</QueryClientProvider>
	);
}

export default App;