import React, { useState } from "react";
import "./App.css";
import Navbar from "./components/Navbar";
import SearchFlights from "./components/SearchFlights";

function App() {
  return (
    <div className="App">
      <Navbar />
      <SearchFlights />
    </div>
  );
}

export default App;
