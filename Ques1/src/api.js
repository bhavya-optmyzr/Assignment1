var MyModel = Backbone.Model.extend(); // Define a model (optional)

var MyCollection = Backbone.Collection.extend({
  model: MyModel,
  initialize: function (options) {
    this.latitude = options.latitude;
    this.longitude = options.longitude;
  },
  url: function () {
    return "https://api.openweathermap.org/data/2.5/weather?q=London,uk&APPID=cddbf0b42be47fe6061841ce93bf9ab6";
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
            console.log(response);
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
