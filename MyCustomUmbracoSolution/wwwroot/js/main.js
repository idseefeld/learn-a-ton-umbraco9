const apiClient = axios.create({
  baseURL: 'https://localhost:44341/umbraco/api',
  withCredentials: false,
  headers: {
    Accept: 'application/json',
    'Content-Type': 'application/json'
  }
});

var app = new Vue({
  el: '#app',
  template:
    /*html*/
    `<div class="speakers" v-if="speakers">
      <h1>{{ speakers.name }}</h1>
      <h2>{{ speakers.description }}</h2>
      <div v-for="speaker in speakers.speakers" class="speaker-container left">
        <div v-if="speaker.image" class="speaker-image left">
          <img v-bind:src="speaker.image">
        </div>
        <div class="speaker-info">
          <h3>{{ speaker.name }}</h3>
          <h4><em>{{ speaker.jobTitle }}</em></h4>
          <p>{{ speaker.description }}</p>
        </div>
      </div>
    </div>`
  ,
  data: {
    speakers: null
  },
  created() {
    this.speakers = this.getSpeakers()
      .then((response) => {
        this.speakers = response.data;
      })
      .catch((error) => {
        console.log(error.toString());
      });
  },
  methods: {
    getSpeakers() {
      return apiClient.get('/speakers/speakers');
    }
  }
})