import ReactDOM from "react-dom/client";
import "./index.css";
import App from "./App";
import Api from "./Api";

ReactDOM.createRoot(document.getElementById("root")).render(
  <Api>
    <App />
  </Api>
);
