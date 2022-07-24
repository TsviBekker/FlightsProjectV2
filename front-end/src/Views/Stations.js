import React, { useState, useEffect } from "react";
import { Loader } from "../Components/Loader/Loader";
import { Operations } from "../Constants/Consts";
import { formatPrepTime } from "../Utils/Utils";
import { StationHistory } from "./StationsHistory/StationHistory";
// import "../App.css";

export const Stations = () => {
  const [stations, setStations] = useState([]);

  useEffect(() => {
    var timout = setTimeout(() => {
      fetch(`${Operations}stations-overview`)
        .then((res) => res.json())
        .then((data) => {
          console.log(data);
          return setStations(data);
        })
        .catch((err) => console.log(err));
    }, 1000);
    return () => clearTimeout(timout);
  }, [stations]);

  if (stations.length === 0) {
    return <Loader />;
  }

  return (
    <div>
      <h1 className="center">Stations Overview</h1>
      <table>
        <thead>
          <tr>
            <th>Station</th>
            <th>Status</th>
            <th>Ocupied By</th>
            <th>Preparation Time</th>
          </tr>
        </thead>

        <tbody>
          {stations.map((station) => {
            return <Station key={station.stationId} station={station} />;
          })}
        </tbody>
      </table>
    </div>
  );
};

const Station = ({ station }) => {
  const [time, setTime] = useState(station.flightInStation?.prepTime);

  useEffect(() => {
    const timeout = setTimeout(() => {
      if (time > 0) {
        setTime(time - 1);
      }
    }, 1000);
    return () => clearTimeout(timeout);
  }, [time, station]);

  return (
    <tr>
      <td>{station.name}</td>
      {station.flight ? (
        <td style={{ color: "#00b040" }}>Available</td>
      ) : (
        <td style={{ color: "red" }}>Unavailable</td>
      )}
      <td>{station.flight ? station.flight.code : "NONE"}</td>
      <td>{station.flight ? formatPrepTime(station.prepTime) : "_-_-_-_"}</td>
      <td>
        <StationHistory station={station} />
      </td>
    </tr>
  );
};
