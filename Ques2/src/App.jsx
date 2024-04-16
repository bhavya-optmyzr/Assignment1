import Button from "react-bootstrap/Button";
import CardStructure from "./components/CardStructure";
import { useState } from "react";

export default function App() {
  const [city, setCity] = useState("London,uk");
  const [input, setInput] = useState("");
  const [cards, setCards] = useState([]);

  function handleClick(input) {
    setCity(input);
    const id = Math.random();
    setCards((prev) => {
      return [...prev, { id: id, city: input }];
    });
  }

  function handleDelete(identifier) {
    setCards((prev) => prev.filter((data) => data.id !== identifier));
  }

  return (
    <>
      {/* <h1>Weather App</h1> */}
      <div id="header">
        <input
          value={input}
          required
          onChange={(event) => {
            setInput(event.target.value);
          }}
        />
        <Button variant="success" onClick={() => handleClick(input)}>
          Add Location
        </Button>
      </div>

      <div id="cards">
        {cards.map((data, index) => {
          return (
            <CardStructure
              key={index}
              city={data.city}
              identifier={data.id}
              onClick={handleClick}
              onDelete={handleDelete}
            />
          );
        })}
      </div>
    </>
  );
}
