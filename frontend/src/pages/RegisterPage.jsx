import { useEffect, useState } from "react";
import useIsAuthenticated from "react-auth-kit/hooks/useIsAuthenticated";
import { Link, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

const LoginPage = () => {

  const isAuthenticated = useIsAuthenticated();
  const navigate = useNavigate();

  useEffect(() => {
    // Redirect to home page if user is already authenticated
    if (isAuthenticated) {
      navigate('/');
    }
  }, [isAuthenticated, navigate]);

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [email, setEmail] = useState("");
  const [deposit, setDeposit] = useState(0);
  const [error, setError] = useState("");

  const handleRegister = async (e) => {
    e.preventDefault();

    try {
      const response = await fetch("https://localhost:7241/api/User/Register", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          userName: username,
          password: password,
          email: email,
          initialDeposit: deposit,
          roles: ["user"],
        }),
      });

      const data = await response.json();

      console.log(data);

      if (data.isSuccess) {
        // user created
        toast.success("Login successful!");
        navigate("/login");
      } else {
        toast.error(data.message);
        setError(data.errors[0]);
      }
    } catch (error) {
      console.error("Login error:", error);
      if (error.message === "Failed to fetch") {
        setError(
          "Failed to connect to the server. Please check your network connection."
        );
      } else {
        setError("An error occured while processing your request.");
      }
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-50 py-12 px-4 sm:px-6 lg:px-8">
      <div className="max-w-md w-full space-y-8">
        <div>
          <h2 className="mt-6 text-center text-3xl font-extrabold text-gray-900">
            Register your account
          </h2>
        </div>
        <div className="text-red-400 text-center">{error}</div>
        <form className="mt-8 space-y-6" onSubmit={handleRegister}>
          <input type="hidden" name="remember" defaultValue="true" />
          <div className="rounded-md shadow-sm ">
            <div>
              <label htmlFor="username" className="sr-only">
                Username
              </label>
              <input
                id="username"
                name="username"
                type="text"
                autoComplete="username"
                required
                className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                placeholder="Username"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
              />
            </div>
            <div className="mt-3">
              <label htmlFor="email" className="sr-only">
                Email
              </label>
              <input
                id="email"
                name="email"
                type="email"
                autoComplete="email"
                required
                className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                placeholder="Email Address"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
              />
            </div>
            <div className="mt-3">
              <label htmlFor="deposit" className="sr-only">
                Initial Deposit
              </label>
              <input
                id="deposit"
                name="deposit"
                type="number"
                required
                className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                placeholder="Intial Deposit Amount"
                value={deposit}
                onChange={(e) => setDeposit(e.target.value)}
              />
            </div>
            <div className="mt-3">
              <label htmlFor="password" className="sr-only">
                Password
              </label>
              <input
                id="password"
                name="password"
                type="password"
                autoComplete="current-password"
                required
                className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                placeholder="Password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />
            </div>
          </div>

          <div className="flex items-center justify-end">
            <div className="text-sm">
              <Link
                to="/login"
                className="font-medium text-indigo-600 hover:text-indigo-500"
              >
                Already have an account? Log in.
              </Link>
            </div>
          </div>

          <div>
            <button
              type="submit"
              className="group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
            >
              Register
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default LoginPage;
