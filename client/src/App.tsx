import React, { useState } from "react";
import "./App.css";
import Navbar from "./components/Navbar";
import SearchFlights from "./components/SearchFlights";
import { BrowserRouter } from "react-router-dom";

function App() {
  return (
    <div className="App">
      <BrowserRouter>
        <Navbar />
        <SearchFlights />
      </BrowserRouter>
    </div>
  );
}

export default App;
