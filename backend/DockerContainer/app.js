const express = require('express');
const app = express();
const router = express.Router();

const port = 8080;

router.get('/', function(_req,res){
    res.send({message: "Hello from Node in Docker"});
  });

app.use('/', router);

app.listen(port, function () {
  console.log('Example app listening on port 8080')
})