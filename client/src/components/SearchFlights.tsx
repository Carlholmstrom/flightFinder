/* eslint-disable jsx-a11y/img-redundant-alt */
import React, { useState } from "react";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { Autocomplete } from "@mui/material";
import { format } from "date-fns";
import SearchIcon from "@mui/icons-material/Search";
import { TextField, Grid, Button, Box } from "@mui/material";
import "./SearchFlights.css";
import image from "../assets/images/airplane-1807486_1920.jpg";

interface Airport {
  code: string;
  name: string;
}
interface FlightRoute {
  departureDestination: string;
  arrivalDestination: string;
  layoverDestination: string | null;
  layoverDuration: string;
  totalPrice: number;
}

export interface Flight {
  flightId: string;
  departureAt: string;
  arrivalAt: string;
  availableSeats: number;
  flightRoute: FlightRoute;
  totalTravelTime: string;
}

const airports: Airport[] = [
  { code: "OSL", name: "Oslo" },
  { code: "STO", name: "Stockholm" },
  { code: "AMS", name: "Amsterdam" },
];

const SearchFlights = () => {
  const [fromAirport, setFromAirport] = useState<Airport | null>(null);
  const [toAirport, setToAirport] = useState<Airport | null>(null);
  const [departureDate, setDepartureDate] = useState<Date | null>(null);
  const [returnDate, setReturnDate] = useState<Date | null>(null);
  const [searchResults, setSearchResults] = useState<Flight[]>([]);

  const handleSearch = async () => {
    try {
      const searchParams = {
        fromAirport: fromAirport?.name,
        toAirport: toAirport?.name,
        departureDate: departureDate
          ? format(departureDate, "yyyy-MM-dd")
          : null,
      };

      const url = `https://localhost:7289/api/Flights/${searchParams.fromAirport}/${searchParams.toAirport}?date=${searchParams.departureDate}`;

      const response = await fetch(url);
      const data = await response.json();

      if (response.ok) {
        const flights = data.map((flightData: any) => {
          const flight: Flight = {
            flightId: flightData.flightId,
            departureAt: flightData.departureAt,
            arrivalAt: flightData.arrivalAt,
            availableSeats: flightData.availableSeats,
            flightRoute: {
              departureDestination: flightData.flightRoute.departureDestination,
              arrivalDestination: flightData.flightRoute.arrivalDestination,
              layoverDestination: flightData.flightRoute.layoverDestination,
              layoverDuration: flightData.flightRoute.layoverDuration,
              totalPrice: flightData.flightRoute.totalPrice,
            },
            totalTravelTime: flightData.totalTravelTime,
          };
          return flight;
        });
        setSearchResults(flights);
      } else {
        throw new Error(`Search failed: ${response.status}`);
      }
    } catch (error) {
      console.error(error);
      alert(`Search failed`);
    }
  };
  return (
    <div>
      <Box display="flex" flexDirection="column" alignItems="center">
        <img
          src={image}
          alt="plane over skyscrapers"
          style={{ width: "100%", height: "300px" }}
        />
        <Grid container spacing={2} justifyContent="center" marginTop={3}>
          <Grid
            item
            xs={12}
            sm={6}
            sx={{ display: "flex", justifyContent: "center" }}
          >
            <Autocomplete
              sx={{ mb: { xs: 2, sm: 0 }, width: "100%", ml: 2, mr: 2 }}
              options={airports}
              getOptionLabel={(option) => `${option.name} (${option.code})`}
              renderInput={(params) => (
                <TextField {...params} label="From" variant="outlined" />
              )}
              onChange={(event, newValue: Airport | null) =>
                setFromAirport(newValue)
              }
            />
          </Grid>
          <Grid
            item
            xs={12}
            sm={6}
            sx={{ display: "flex", justifyContent: "center" }}
          >
            <Autocomplete
              sx={{ mb: 2, width: "100%", mr: 2, ml: 2 }}
              options={airports}
              getOptionLabel={(option) => `${option.name} (${option.code})`}
              renderInput={(params) => (
                <TextField {...params} label="To" variant="outlined" />
              )}
              onChange={(event, newValue: Airport | null) =>
                setToAirport(newValue)
              }
            />
          </Grid>
        </Grid>

        <Grid container spacing={2} justifyContent="center">
          <Grid item>
            <DatePicker
              selected={departureDate}
              onChange={(date: Date | null) => setDepartureDate(date)}
              dateFormat="MM/dd/yyyy"
              className="form-control"
              placeholderText="Departure Date"
            />
          </Grid>
          <Grid item>
            <DatePicker
              selected={returnDate}
              onChange={(date: Date | null) => setReturnDate(date)}
              dateFormat="MM/dd/yyyy"
              className="form-control"
              placeholderText="Return Date"
            />
          </Grid>
        </Grid>

        <Button
          sx={{
            mt: 2,
            backgroundColor: "#6F9DFE",
            color: "white",
            borderRadius: "30px",
            "&:hover": {
              backgroundColor: "#6F9DFE",
            },
            "&:active": {
              backgroundColor: "#92B4F2",
            },
          }}
          variant="contained"
          onClick={handleSearch}
          startIcon={<SearchIcon />}
        >
          Search flights
        </Button>

        {searchResults.length > 0 && (
          <Box mt={4}>
            <h2>
              Flights from {fromAirport?.name} to {toAirport?.name} on{" "}
              {departureDate && format(departureDate, "yyyy-MM-dd")}
            </h2>

            <ul>
              {searchResults.map((flight) => (
                <li key={flight.flightId}>
                  <p>Flight ID: {flight.flightId}</p>
                  <p>Departure Time: {flight.departureAt}</p>
                  <p>Arrival Time: {flight.arrivalAt}</p>
                  <p>Available Seats: {flight.availableSeats}</p>
                  <p>Total Travel Time: {flight.totalTravelTime}</p>
                  <p>
                    Departure Destination:{" "}
                    {flight.flightRoute.departureDestination}
                  </p>
                  <p>
                    Arrival Destination: {flight.flightRoute.arrivalDestination}
                  </p>
                  <p>
                    Layover Destination:{" "}
                    {flight.flightRoute.layoverDestination || "N/A"}
                  </p>
                  <p>Layover Duration: {flight.flightRoute.layoverDuration}</p>
                  <p>Total Price: {flight.flightRoute.totalPrice}</p>
                </li>
              ))}
            </ul>
          </Box>
        )}
      </Box>
    </div>
  );
};

export default SearchFlights;
