var MyModel = Backbone.Model.extend();

var MyCollection = Backbone.Collection.extend({
  model: MyModel,
  initialize: function (options) {
    this.latitude = options.latitude;
    this.longitude = options.longitude;
  },
  url: function () {
    return `https://api.openweathermap.org/data/2.5/weather?q=Delhi,in&APPID=cddbf0b42be47fe6061841ce93bf9ab6`;
  },
});

// Function to create a new card element with weather information
function createCard(location, temperature, description) {
  var cardDiv = document.createElement("div");
  cardDiv.className = "card";
  cardDiv.style = "width: 18rem; margin-bottom: 10px;";

  var cardBody = document.createElement("div");
  cardBody.className = "card-body";

  var title = document.createElement("h5");
  title.className = "card-title";
  title.innerText = location;

  var temp = document.createElement("p");
  temp.className = "card-text";
  temp.innerText = "Temperature: " + temperature + " °C";

  var desc = document.createElement("p");
  desc.className = "card-text";
  desc.innerText = "Description: " + description;

  cardBody.appendChild(title);
  cardBody.appendChild(temp);
  cardBody.appendChild(desc);

  cardDiv.appendChild(cardBody);

  return cardDiv;
}

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
    var location = document.getElementById("locationInput").value; // Get location from input field
    if (!location) {
      alert("Please enter a location");
      return;
    }
    var url = `https://api.openweathermap.org/data/2.5/weather?q=${location}&APPID=cddbf0b42be47fe6061841ce93bf9ab6`;

    fetch(url)
      .then(response => response.json())
      .then(data => {
        // Create a new card element with the fetched data
        var newCard = createCard(data.name, (data.main.temp - 273).toFixed(2), data.weather[0].description);

        // Append the new card to the container in the DOM
        var container = document.getElementById("cardsContainer");
        container.appendChild(newCard);
      })
      .catch(error => {
        console.error("Error fetching data:", error);
        self.$el.html("Error fetching data. See console for details.");
      });
      
  },
});

var myView = new MyView();

// Add event listener to Fetch Weather button
document.getElementById("fetchWeatherBtn").addEventListener("click", function() {
  myView.fetchData();
});
