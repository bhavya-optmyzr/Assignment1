import { createContext, useState } from "react";
import { thisCity } from "./App";

var MyModel = Backbone.Model.extend(); // Define a model (optional)

export const Context=createContext({city:"", weatherMain:""});



export default function Api({children}) {
  const [city, setCity]=useState("");
  const [weatherMain, setWeatherMain] = useState("");

  var MyCollection = Backbone.Collection.extend({
    model: MyModel,
    initialize: function (options) {
      this.latitude = options.latitude;
      this.longitude = options.longitude;
    },
    url: function () {
      console.log("calling api")
      const response = `https://api.openweathermap.org/data/2.5/weather?q=${thisCity}&APPID=cddbf0b42be47fe6061841ce93bf9ab6`;
      console.log("api called");
      return response;
      // return 'https://api.openweathermap.org/data/3.0/onecall?lat=' + this.latitude + '&lon=' + this.longitude + '&exclude=daily&appid=cddbf0b42be47fe6061841ce93bf9ab6';
    },
  });
  
  
  var MyView = Backbone.View.extend({
    el: "#api",
    initialize: function () {
      this.render();
    },
    render: function () {
      this.$el.html("Loading...");
      this.fetchData();
    },
    fetchData: function () {
      var self = this;
      navigator.geolocation.getCurrentPosition(
        function (position) {
          var latitude = position.coords.latitude;
          var longitude = position.coords.longitude;
  
          var myData = new MyCollection({
            latitude: latitude,
            longitude: longitude,
          });
  
  
          myData.fetch({
            success: function (collection, response) {
              console.log(response.name)
              setCity(response.name);
              // console.log(response.weather[0].main);
              setWeatherMain(response.weather[0].main);

              // console.log(response);
              // console.log(weatherMain);
              // // console.log(city);
              self.$el.html(
                "Data fetched successfully: " + JSON.stringify(response)
              );
            },
            error: function (collection, response) {
              self.$el.html("Error fetching data: " + JSON.stringify(response));
            },
          });
        },
        function (error) {
          self.$el.html("Error getting user location: " + JSON.stringify(error));
        }
      );
    },
  });
  
  var myView = new MyView();
  // console.log(city);
  return <Context.Provider value={{city , weatherMain}}>
    {children}
  </Context.Provider>
}