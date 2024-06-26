import Button from "react-bootstrap/Button";
import CardStructure from "./components/CardStructure";
import { useState } from "react";

export default function App() {
  const [city, setCity] = useState("London,uk");
  const [input, setInput] = useState("");
  const [cards, setCards] = useState([]);

  function handleClick() {
    setCity(input);
    const id = cards.length + 1;
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
        <Button variant="success" onClick={() => handleClick()}>
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
              onDelete={handleDelete}
            />
          );
        })}
      </div>
    </>
  );
}
