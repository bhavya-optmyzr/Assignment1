import CardStructure from "./components/CardStructure";
import {useContext} from 'react';
import { Context } from "./Api";

export const thisCity = "London,uk";

export default function App() {
  const {city}=useContext(Context);
  const {weatherMain}=useContext(Context);
 
  return (
    <>
      <h1>Weather App</h1>
      
      <div id="cards">
        <CardStructure city={city} weatherMain={weatherMain}/>
        {city}
      </div>

    </>
  );
}

