import { useState } from "react";
import useAuthHeader from "react-auth-kit/hooks/useAuthHeader";
import useAuthUser from "react-auth-kit/hooks/useAuthUser";
import useSignOut from "react-auth-kit/hooks/useSignOut";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

const HomePage = () => {
  const token = useAuthHeader();
  const auth = useAuthUser();
  const signOut = useSignOut();
  const navigate = useNavigate();
  const [balanceModal, showBalanceModal] = useState(false);
  const [depositModal, showDepositModal] = useState(false);
  const [withdrawalModal, showWithdrawalModal] = useState(false);
  const [balance, setBalance] = useState("XX");
  const [deposit, setDeposit] = useState(0);
  const [withdraw, setWithdraw] = useState(0);
  const [password, setPassword] = useState("");

  const handleSignout = () => {
    signOut();
    toast.success("Logged out successfully!");
    navigate("/login");
  };

  const handleBalanceInquiry = async () => {
    showBalanceModal(true);
    try {
      const response = await fetch(
        "https://localhost:7241/api/Account/Balance-Inquiry",
        {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
            Authorization: `${token}`,
          },
        }
      );

      const data = await response.json();

      if (!response.ok) {
        throw new Error();
      }

      console.log(data);

      if (data.isSuccess) {
        // user created
        toast.success("Balance fetched successfully.");
        setBalance(data.balance);
      } else {
        toast.error("Error fetching balance. Can't connect to the server.");
      }
    } catch (error) {
      toast.error("Error fetching balance. Can't connect to the server.");
    }
  };

  const showDeposit = () => {
    showDepositModal(true);
  };

  const handleDeposit = async (e) => {
    e.preventDefault();

    const depositPromise = fetch(
      `https://localhost:7241/api/Account/Deposit?amount=${deposit}`,
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `${token}`, // Ensure the token is passed correctly
        },
      }
    ).then((response) => {
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      return response.json();
    });

    toast
      .promise(depositPromise, {
        pending: "Processing your deposit...",
        success: {
          render({ data }) {
            closeModal();
            return data.message;
          },
        },
        error: {
          render({ data }) {
            return data.message || "Error processing your request.";
          },
        },
      })
      .catch((error) => {
        console.error("Error processing deposit:", error);
        toast.error("Error processing your request.");
      });
  };

  const showWithdraw = () => {
    showWithdrawalModal(true);
  };

  const handleWithdraw = async (e) => {
    e.preventDefault();

    const withdrawPromise = fetch(
      `https://localhost:7241/api/Account/Withdraw`,
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `${token}`,
        },
        body: JSON.stringify({
          amount: withdraw,
          password: password,
        }),
      }
    ).then((response) => {
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      return response.json();
    });

    toast
      .promise(withdrawPromise, {
        pending: "Processing your withdrawal...",
        success: {
          render({ data }) {
            closeModal();
            return data.message;
          },
        },
        error: {
          render({ data }) {
            return data.message || "Error processing your request.";
          },
        },
      })
      .catch((error) => {
        console.error("Error processing withdrawal:", error);
        toast.error("Error processing your request.");
      });
  };

  const closeModal = () => {
    showBalanceModal(false);
    showDepositModal(false);
    showWithdrawalModal(false);
    setPassword("");
    setDeposit("");
  };

  return (
    <div className="m-3">
      <div className="flex justify-between">
        <p className="text-3xl">Hello👋, {auth.name.username}!</p>
        <button
          onClick={() => handleSignout()}
          className="bg-slate-900 hover:bg-slate-700 text-white font-bold py-2 px-4 rounded-full"
        >
          Logout
        </button>
      </div>
      <div className="mt-5">
        Welcome to your account!
        <hr />
      </div>
      <div className="flex justify-start gap-3 mt-5">
        <div className="bg-teal-500 p-5 rounded">
          <p className="text-2xl">Balance Inquiry</p>
          <button
            onClick={() => handleBalanceInquiry()}
            className="mt-3 bg-teal-200 rounded py-1 px-3"
          >
            Check
          </button>
        </div>
        <div className="bg-teal-500 p-5 rounded">
          <p className="text-2xl">Deposit Amount</p>
          <button
            onClick={() => showDeposit()}
            className="mt-3 bg-teal-200 rounded py-1 px-3"
          >
            Deposit
          </button>
        </div>
        <div className="bg-teal-500 p-5 rounded">
          <p className="text-2xl">Withdraw Amount</p>
          <button
            onClick={() => showWithdraw()}
            className="mt-3 bg-teal-200 rounded py-1 px-3"
          >
            Withdraw
          </button>
        </div>
      </div>

      {/* Modal or Popup */}
      {balanceModal && (
        <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50">
          <div className="w-full md:w-1/3 bg-white p-8 rounded-lg shadow-lg">
            <h2 className="text-2xl mb-4">Balance Inquiry</h2>
            <p className="mb-4">Current balance: Rs. {balance}</p>
            <div className="flex justify-end">
              <button
                onClick={closeModal}
                className="bg-gray-300 hover:bg-gray-400 text-gray-800 font-bold py-2 px-4 rounded mr-2"
              >
                Close
              </button>
              {/* Additional action buttons */}
            </div>
          </div>
        </div>
      )}

      {depositModal && (
        <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50">
          <div className="w-full md:w-1/3 bg-white p-8 rounded-lg shadow-lg">
            <h2 className="text-2xl mb-4">Deposit Amount</h2>
            <form onSubmit={handleDeposit}>
              <div className="mt-3">
                <label htmlFor="deposit" className="sr-only">
                  Amount
                </label>
                <input
                  id="deposit"
                  name="deposit"
                  type="number"
                  required
                  min={100}
                  className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                  placeholder="Deposit Amount"
                  value={deposit}
                  onChange={(e) => setDeposit(e.target.value)}
                />
              </div>
              <div className="mt-3">
                <button
                  type="submit"
                  className="group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
                >
                  Confirm deposit
                </button>
              </div>
            </form>
            <div className="flex justify-end mt-3">
              <button
                onClick={closeModal}
                className="bg-gray-300 hover:bg-gray-400 text-gray-800 font-bold py-2 px-4 rounded mr-2"
              >
                Close
              </button>
              {/* Additional action buttons */}
            </div>
          </div>
        </div>
      )}

      {withdrawalModal && (
        <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50">
          <div className="w-full md:w-1/3 bg-white p-8 rounded-lg shadow-lg">
            <h2 className="text-2xl mb-4">Withdraw Amount</h2>
            <form onSubmit={handleWithdraw}>
              <div className="mt-3">
                <label htmlFor="deposit" className="sr-only">
                  Amount
                </label>
                <input
                  id="deposit"
                  name="deposit"
                  type="number"
                  required
                  min={100}
                  className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                  placeholder="Withdrawal amount"
                  value={withdraw}
                  onChange={(e) => setWithdraw(e.target.value)}
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
                  required
                  className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                  placeholder=" Password"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                />
              </div>
              <div className="mt-3">
                <button
                  type="submit"
                  className="group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
                >
                  Withdraw amount
                </button>
              </div>
            </form>
            <div className="flex justify-end mt-3">
              <button
                onClick={closeModal}
                className="bg-gray-300 hover:bg-gray-400 text-gray-800 font-bold py-2 px-4 rounded mr-2"
              >
                Close
              </button>
              {/* Additional action buttons */}
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default HomePage;
