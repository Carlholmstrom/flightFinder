import React from "react";
import MenuIcon from "@mui/icons-material/Menu";
import FlightIcon from "@mui/icons-material/Flight";
import HotelIcon from "@mui/icons-material/Hotel";
import CarRentalIcon from "@mui/icons-material/DriveEta";

import {
  List,
  ListItem,
  Drawer,
  Toolbar,
  ListItemIcon,
  ListItemText,
  IconButton,
  Typography,
} from "@mui/material";

const Navbar = () => {
  const [open, setOpen] = React.useState(false);

  const handleDrawerOpen = () => {
    setOpen(true);
  };

  const handleDrawerClose = () => {
    setOpen(false);
  };

  return (
    <div>
      <Toolbar style={{ backgroundColor: "#fff" }}>
        <IconButton
          edge="start"
          style={{ marginRight: "16px" }}
          color="inherit"
          aria-label="menu"
          onClick={handleDrawerOpen}
        >
          <MenuIcon style={{ color: "#000" }} />
        </IconButton>
        <Typography variant="h6" style={{ color: "#000" }}>
          Salt Flights
        </Typography>
      </Toolbar>
      <Drawer open={open} onClose={handleDrawerClose}>
        <div style={{ width: "250px" }}>
          <List>
            <ListItem
              button
              style={{ paddingTop: "16px", paddingBottom: "16px" }}
            >
              <ListItemIcon>
                <FlightIcon />
              </ListItemIcon>
              <ListItemText primary="Flights" style={{ color: "#000" }} />
            </ListItem>
            <ListItem
              button
              style={{ paddingTop: "16px", paddingBottom: "16px" }}
            >
              <ListItemIcon>
                <HotelIcon />
              </ListItemIcon>
              <ListItemText
                primary="Hotels (coming soon)"
                style={{ color: "#000" }}
              />
            </ListItem>
            <ListItem
              button
              style={{ paddingTop: "16px", paddingBottom: "16px" }}
            >
              <ListItemIcon>
                <CarRentalIcon />
              </ListItemIcon>
              <ListItemText
                primary="Car Rentals (coming soon)"
                style={{ color: "#000" }}
              />
            </ListItem>
          </List>
        </div>
      </Drawer>
    </div>
  );
};
export default Navbar;
