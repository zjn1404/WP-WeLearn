import { Routes, Route } from "react-router-dom";
import PaymentSuccess from "./components/Payment/PaymentSuccess";
import PaymentFailed from "./components/Payment/PaymentFailed";

function App() {
  return (
    <Routes>
      <Route path="payment-success" element={<PaymentSuccess />} />
      <Route path="payment-failed" element={<PaymentFailed />} />
      <Route path="*" element={<h1>404 NOT FOUND</h1>} />
    </Routes>

  );
}

export default App;
