(function () {
    var app = angular.module('TeamStatus');

    app.controller('ProfileController', function (userdata,userService,ProfileService) {

        var profile = this;
        profile.AddMode = false;
        profile.name = userService.Name;
        var imageBasePath = 'http://localhost:63214/App_Views/User/Profile/DP/';
        profile.obj = {};
        

        if (_.isUndefined(userdata) || _.isNull(userdata) || _.isEmpty(userdata)) {
            profile.AddMode = !profile.AddMode;
        }

        profile.editProfile = function() {
            profile.AddMode = !profile.AddMode;
        }

        if (_.isUndefined(userdata.usrImage) || _.isNull(userdata.usrImage) || _.isEmpty(userdata.usrImage)) {
            profile.image = imageBasePath + 'blank_user.png';
        } else {
            profile.image = userdata.usrImage;
        }

        profile.saveImage = function(fileInfo) {

            profile.obj.flow.target = imageBasePath + profile.name;
            ProfileService.SaveProfilePicture(fileInfo);
        }

        profile.onUploadCompleted = function() {
            
        }

        profile.onFlowError = function(file, msg, flow) {
            console.log(file);
            console.log(msg);
            console.log(flow);
        };

    });
})();