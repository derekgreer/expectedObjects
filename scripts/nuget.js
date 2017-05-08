var exec = require('child_process').exec,                                                                                      
  extend = require('extend'),
  fs = require('fs'),
  path = require('path'),
  request = require('request'),
  tmp = require('tmp'),
  which = require('which'),
  argv = require('minimist')(process.argv.slice(2)),
  args = process.argv.slice(2).join(' '),                                                                                      
  options = {},
  defaults = {
    silent: false
  };

extend(options, defaults, argv);
options.verbose = !options.silent;

which('nuget.exe', function(err, resolvedPath) {
  var nugetPath;

  if(err) {
    var tmpdir = tmp.dirSync();
    nugetPath = path.join(tmpdir.name, "nuget.exe");

    options.verbose && console.log("downloading nuget.exe from distribution.");
    request.get('https://dist.nuget.org/win-x86-commandline/latest/nuget.exe')
      .on('error', function(err) {
        console.log(err)
      })
      .pipe(fs.createWriteStream(nugetPath))
      .on('close', function() {
        nuget(nugetPath, args);
      });
  }
  else {
    nuget(resolvedPath, args);
  }
});

function nuget(nugetPath, args) {
  console.log(`Executing: ${nugetPath} ${args}`);
  exec(`${nugetPath} ${args}`, function(error, stdout, stderr) {
    console.log(stdout);
    console.log(stderr);
  });
}

