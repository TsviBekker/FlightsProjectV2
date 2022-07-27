import React from "react";
import "./NavBar.css";
import { NavLink } from "react-router-dom";
import {
  FaPlane,
  FaPlaneArrival,
  FaPlaneDeparture,
  FaChargingStation,
  FaFileAlt,
} from "react-icons/fa";
import { BiStation } from "react-icons/bi";

export const NavBar = () => {
  return (
    <nav className="nav">
      <div className="container">
        <h4 className="site-title">
          Control Center
          <NavLink to="/">
            <FaPlane />
          </NavLink>
        </h4>
      </div>
      <ul>
        <div className="container">
          <FaChargingStation />
          <CustomLink to="/stations">
            <h5>Stations Overview</h5>
          </CustomLink>
          <BiStation />
        </div>
        <div className="container">
          <FaPlaneArrival />
          <CustomLink to="/arriving-flights">
            <h5>Arriving Flights</h5>
          </CustomLink>
          <FaPlaneArrival />
        </div>
        <div className="container">
          <FaPlaneDeparture />
          <CustomLink to="/departing-flights">
            <h5>Departing Flights</h5>
          </CustomLink>
          <FaPlaneDeparture />
        </div>
      </ul>
      <NavLink to="/">
        <h6 className="docu">Docs</h6>
        <FaFileAlt />
      </NavLink>
    </nav>
  );
};

const CustomLink = ({ to, children, ...props }) => {
  const path = window.location.pathname;

  return (
    <li className={path === to ? "active" : ""}>
      <NavLink to={to} {...props}>
        {children}
      </NavLink>
    </li>
  );
};
