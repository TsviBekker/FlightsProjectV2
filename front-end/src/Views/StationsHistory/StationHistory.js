import React, { useState } from "react";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import { Typography } from "@mui/material";
import Modal from "@mui/material/Modal";
import { formatDateTime } from "../../Utils/Utils";
import { Operations } from "../../Constants/Consts";
import { FaHistory} from "react-icons/fa";

const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 400,
  bgcolor: "background.paper",
  border: "2px solid #000",
  boxShadow: 24,
  p: 4,
};

export const StationHistory = ({ station }) => {
  const [open, setOpen] = useState(false);
  const [history, setHistory] = useState([]);

  const handleOpen = () => {
    setOpen(true);
    fetch(`${Operations}get-station-history/${station.stationId}`)
      .then((res) => res.json())
      .then((data) => setHistory(data));
  };

  return (
    <div>
      <Button onClick={handleOpen}>
        View History
        <FaHistory style={{ "margin-left": "10px" }} />
      </Button>
      <Modal open={open} onClose={() => setOpen(false)}>
        <Box sx={style} className="modal">
          <Typography id="modal-modal-title" variant="h6" component="h2">
            {station.name} History
          </Typography>

          {history.length === 0 ? (
            <h2>"NO HISTORY FOR NOW..."</h2>
          ) : (
            <table>
              <thead>
                <tr>
                  <th>flight id</th>
                  <th>Code</th>
                  <th>Entered At</th>
                  <th>Left At</th>
                </tr>
              </thead>
              <tbody>
                {history.map((h) => {
                  return (
                    <tr>
                      <td>{h.flight.flightId}</td>
                      <td>{h.flight.code}</td>
                      <td>{formatDateTime(h.entered)}</td>
                      <td>{formatDateTime(h.left)}</td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          )}
        </Box>
      </Modal>
    </div>
  );
};
