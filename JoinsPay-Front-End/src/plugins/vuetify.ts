import Vue from 'vue';
import Vuetify from 'vuetify';
import 'vuetify/dist/vuetify.min.css';
import pt from 'vuetify/src/locale/pt';

Vue.use(Vuetify);

export default new Vuetify({
  theme: {
      options: {
        customProperties: true,
      },
    themes: {
      light: {
        primary: '#1b023a',
        secondary: '#BF360C',
        accent: '#82B1FF',
        error: '#FF5252',
        info: '#2196F3',
        success: '#4CAF50',
        warning: '#FFC107',
        colorPrimaryText: 'black',
        colorMenuText: 'white',
        colorTableText: 'white'
      },
    },
  },
    lang: {
      locales: { pt },
      current: 'pt',
    },
});
