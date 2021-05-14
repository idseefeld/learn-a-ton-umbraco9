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
    `<div class="speakers">
      <h1>{{ speakers.name }}</h1>
      <h2>{{ speakers.title }}</h2>
      <div v-for="speaker in speakers.speakers" :key="name" class="speaker-container">
        <div v-if="speaker.picture" class="speaker-image">
          <img v-bind:src="speaker.picture">
        </div>
        <div class="speaker-info">
          <h3>{{ speaker.fullName }}</h3>
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