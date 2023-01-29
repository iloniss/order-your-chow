import axios from 'axios';

const baseURL = 'http://localhost:8080/';

export default axios.create({
  baseURL: baseURL + 'api',
  headers: {
    'Content-type': 'application/json',
    'Access-Control-Allow-Origin': '*',
  },
});
