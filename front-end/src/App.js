import React from "react";
import { Routes, Route } from "react-router-dom";
import { NavBar } from "./Components/NavBar/NavBar";
import { Stations } from "./Views/Stations";
import { FlightHistory } from "./Views/FlightHistory";
import "./App.css";
import { Home } from "./Views/Home/Home";

export const App = () => {
  return (
    <>
      <NavBar />
      <div className="center">
        <Routes>
          <Route
            path="arriving-flights"
            element={<FlightHistory type="af" />}
          ></Route>
          <Route
            path="departing-flights"
            element={<FlightHistory type="df" />}
          ></Route>
          <Route path="stations" element={<Stations />}></Route>
          <Route path="/" element={<Home />}></Route>
        </Routes>
      </div>
    </>
  );
};
