import React from "react";
import "./Home.css";
import diagram1 from "../../images/diagram1.png";

export const Home = () => {
  return (
    <main>
      <h2>Flight Management Project</h2>
      <p>To start press any one of the links above</p>
      <ul>
        <li>
          <h5>System Overview:</h5>
          <p>
            The system simulates the management of an airport, as flights depart
            and arrive into the airport they go through different stations.
          </p>
        </li>
        <li>
          <h5>Architecture</h5>
          <div className="container">
            <div>
              <p>The program consists of two projects:</p>
              <ul>
                <li>The Backend: an ASP.Net Core API. project</li>
                <li>The Frontend: a web UI with React.</li>
              </ul>
              <li>
                <h5>The background service:</h5>
                <p>runs in the background and does the following:</p>
                <ul>
                  <li>Simulates flights entering the airport</li>
                  <li>Simulates flights' preparation in the station</li>
                  <li>Simulates flights' moving from station to station</li>
                </ul>
              </li>
            </div>
            <img src={diagram1} />
          </div>
        </li>
        {/* <img src="public/images/diagram1.png" /> */}
      </ul>
    </main>
  );
};
