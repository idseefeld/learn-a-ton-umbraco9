app.component('speaker-list', {
  name: 'speaker-list',
  template:
    /*html*/
    `<div class="speakers">
      <h1>{{ name }}</h1>
      <h2>{{title}}</h2>
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
    title: '',
    name: '',
    speakers: null
  },
  created() {
    this.speakers = this.getSpeakers()
      .then((response) => {
        this.speakers = response.data;
        this.message = this.speakers.name + ' | ' + this.speakers.title + ' | ';
        for (let n = 0; n < this.speakers.speakers.length; n++) {
          this.message += ' | ' + this.speakers.speakers[n].fullName;
        }
        console.log(this.speakers);
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