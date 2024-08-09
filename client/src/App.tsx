import { useEffect, useState } from "react";
import "./App.css";
import { Api, LoginRequest, WeatherForecast } from "./api";
import { AxiosResponse } from "axios";

const api = new Api();

function App() {
  const [credential, setCredential] = useState<LoginRequest>({
    email: "",
    password: "",
  });

  const [status, setStatus] = useState<string | null>(null);
  const [data, setData] = useState<WeatherForecast[]>([]);

  useEffect(() => {
    loadData();
  }, []);

  async function loadData() {
    try {
      const response = await api.weatherForecast.getWeatherForecast();
      setStatus(response.statusText);
      setData(response.data);
    } catch (e) {
      const error = e as AxiosResponse;
      setStatus(error.statusText);
      setData([]);
    }
  }

  async function login() {
    const response = await api.login.loginCreate(credential, {
      useCookies: true,
    });
    loadData();
  }

  async function register() {
    await api.register.registerCreate(credential);
  }

  return (
    <>
      <div className="hero min-h-screen bg-base-200">
        <div className="hero-content flex-col lg:flex-row-reverse">
          <div className="text-center lg:text-left">
            <h1 className="text-5xl font-bold">Login now!</h1>
            <div className="form-control">
              <label className="label">
                <span className="label-text">Email</span>
              </label>
              <input
                type="text"
                placeholder="email"
                className="input input-bordered"
                onChange={(e) =>
                  setCredential({ ...credential, email: e.target.value })
                }
              />
            </div>
            <div className="form-control">
              <label className="label">
                <span className="label-text">Password</span>
              </label>
              <input
                type="password"
                placeholder="password"
                className="input input-bordered"
                onChange={(e) =>
                  setCredential({ ...credential, password: e.target.value })
                }
              />
            </div>
            <div className="form-control mt-6">
              <button className="btn btn-primary" onClick={() => register()}>
                Register
              </button>
            </div>
            <div className="form-control mt-6">
              <button className="btn btn-secondary" onClick={() => login()}>
                Login
              </button>
            </div>
          </div>
          <div className="card">
            <div className="card-title">{status}</div>
            <div className="card-body">{JSON.stringify(data)}</div>
          </div>
        </div>
      </div>
    </>
  );
}

export default App;
