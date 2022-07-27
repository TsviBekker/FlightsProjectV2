import React from "react";
import "./Home.css";
import diagram1 from "../../images/diagram1.png";
import diagram2 from "../../images/diagram2.png";
import diagram3 from "../../images/diagram3.png";

export const Home = () => {
  return (
    <main>
      <h2>Flight Management Project</h2>
      <a
        target="_blank"
        className="link"
        href="https://github.com/TsviBekker/FlightsProjectV2.git"
      >
        GitHub
      </a>
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
                <h5>The background service</h5>
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
          <li>
            <h5>The UI (User Interface)</h5>
            <p>
              The UI is a single page application made with the React framework.
            </p>
            <p>
              *In the current version the UI consists only of a navigation bar
              and a display for each navigation option, as shown above
            </p>
          </li>
          <li>
            <h5>Use Cases, Sequence Digram</h5>
            <img src={diagram2} />
          </li>
          <li>
            <h3>Flight Routes:</h3>
            <img src={diagram3} />
          </li>
        </li>
      </ul>
    </main>
  );
};
