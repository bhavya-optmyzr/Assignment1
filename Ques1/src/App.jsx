import CardStructure from "./components/CardStructure";
import {useContext, useState} from 'react';
import { Context } from "./Api";

export const thisCity = "London,uk";

export default function App() {
  const {city}=useContext(Context);
  const {weatherMain}=useContext(Context);

  const [cards, setCards]=useState([""]);

  function handleClick(input){
    setCards(prev => {
      return [...prev, input];
    })
  }
 
  return (
    <>
      <h1>Weather App</h1>
      
      <div id="cards">
        {cards.map((data, index) => {
          return <CardStructure key={index} city={data} weatherMain={weatherMain} onClick={handleClick}/>
        })}
        
        {city}
      </div>

    </>
  );
}

