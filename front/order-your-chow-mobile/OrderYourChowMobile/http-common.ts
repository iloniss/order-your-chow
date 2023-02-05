import axios from 'axios';

const baseURL = 'http://10.0.2.2:5000/';

export default axios.create({
  baseURL: baseURL + 'api',
  headers: {
    'Content-type': 'application/json',
    'Access-Control-Allow-Origin': '*',
  },
});
