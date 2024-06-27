import {
  RouterProvider,
  createBrowserRouter,
  createRoutesFromElements,
  Route,
} from "react-router-dom";
import createStore from "react-auth-kit/createStore";
import AuthProvider from "react-auth-kit/AuthProvider";

import AuthOutlet from "@auth-kit/react-router/AuthOutlet";

import LoginPage from "./pages/LoginPage";
import RegisterPage from "./pages/RegisterPage";
import HomePage from "./pages/HomePage";

import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

const App = () => {
  const store = createStore({
    authName: "_auth",
    authType: "cookie",
    cookieDomain: window.location.hostname,
    cookieSecure: false,
  });

  const router = createBrowserRouter(
    createRoutesFromElements(
      <Route>
        <Route path={"/register"} element={<RegisterPage />} />
        <Route path={"/login"} element={<LoginPage />} />
        <Route element={<AuthOutlet fallbackPath="/login" />}>
          <Route path="/" element={<HomePage />} />
        </Route>
      </Route>
    )
  );

  return (
    <AuthProvider store={store}>
      <RouterProvider router={router} />
      <ToastContainer />
    </AuthProvider>
  );
};

export default App;
