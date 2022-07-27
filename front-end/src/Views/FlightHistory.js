import React, { useEffect, useState } from "react";
import { Operations } from "../Constants/Consts";
import { formatDateTime } from "../Utils/Utils";
import { Loader } from "../Components/Loader/Loader";
import { FaPlane } from "react-icons/fa";

export const FlightHistory = ({ type }) => {
  const [history, setHistory] = useState([]);

  useEffect(() => {
    const timeout = setTimeout(() => {
      fetch(`${Operations}get-${type}-history`)
        .then((res) => res.json())
        .then((data) => {
          setHistory(
            data.reduce((acc, curr) => {
              if (!acc[curr["flight"]]) {
                acc[curr["flight"]] = [];
              }
              acc[curr["flight"]].push(curr);
              return acc;
            }, {})
          );
          console.log(data, "DATA");
          console.log(history, "HISTORY");
        });
    }, 1000);
    return () => clearTimeout(timeout);
  }, [history]);

  if (history.length === 0) {
    return <Loader />;
  }

  return (
    <>
      <h2>{type === "af" ? "Arriving" : "Departing"} Flights</h2>
      <div className="history-container">
        {Object.entries(history).map(([flight, arr]) => {
          return (
            <table>
              <thead>
                <th colSpan={4}>
                  <p style={{ margin: "0" }}>
                    <FaPlane /> Flight: {flight} <FaPlane />
                  </p>
                </th>
                <tr>
                  <th>Station</th>
                  <th>Entered</th>
                  <th>Left</th>
                </tr>
              </thead>
              <tbody>
                {arr.map((hist) => {
                  return (
                    <tr>
                      <td>{hist.station.name}</td>
                      <td>{formatDateTime(hist.entered)}</td>
                      <td>{formatDateTime(hist.left)}</td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          );
        })}
      </div>
    </>
  );
};
