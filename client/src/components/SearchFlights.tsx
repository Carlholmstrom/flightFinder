import React, { useState } from "react";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { Autocomplete } from "@mui/material";
import { format } from "date-fns";
import { TextField, Grid, Button, Box } from "@mui/material";
import "./SearchFlights.css";

interface Airport {
  code: string;
  name: string;
}

export interface Flight {
  flightId: string;
  departureAt: string;
  arrivalAt: string;
  availableSeats: number;
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
        console.log(data);
        alert("Search successful!");
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
        <Autocomplete
          sx={{ mb: 2, width: "60%" }}
          options={airports}
          getOptionLabel={(option) => `${option.name} (${option.code})`}
          renderInput={(params) => (
            <TextField {...params} label="From" variant="outlined" />
          )}
          onChange={(event, newValue: Airport | null) =>
            setFromAirport(newValue)
          }
        />
        <Autocomplete
          sx={{ mb: 2, width: "60%" }}
          options={airports}
          getOptionLabel={(option) => `${option.name} (${option.code})`}
          renderInput={(params) => (
            <TextField {...params} label="To" variant="outlined" />
          )}
          onChange={(event, newValue: Airport | null) => setToAirport(newValue)}
        />
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
          sx={{ mt: 2 }}
          variant="contained"
          color="primary"
          onClick={handleSearch}
        >
          Search
        </Button>
      </Box>
    </div>
  );
};

export default SearchFlights;
