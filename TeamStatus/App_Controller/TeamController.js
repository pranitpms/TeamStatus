(function() {

    angular.module('TeamStatus')
        .controller('TeamController', function (teamList, adminService, toastr) {

            var team = this;
            team.data = [{}];
            team.addMode = false;
            team.persistObject = {};


            team.config = {
                columns: [
                    { name: 'teamName', lable: 'Team Name', type: 'text', isVisible: true },
                    { name: 'teamId', lable: '', type: 'text' ,isVisible : false}
                ]
            };

            team.data = teamList;

            team.toggleAddMode = function() {
                team.addMode = !team.addMode;
                team.object = { teamName: '' };
            }

            team.toggleEditMode = function (object) {
                team.persistObject = {};
                object.editMode = !object.editMode;
                team.persistObject = angular.copy(object);
            };

            team.addObject = function() {

                if (_.isUndefined(team.object)) return;

                var teamEntity = {
                    TeamName : team.object.teamName
                }

                var promise = adminService.AddTeam(teamEntity);

                promise.then(function (result) {
                    team.data.push(result);
                }).catch(function(error) {
                    toastr.error(error);
                });
            };

            team.Ischanged = function(entity) {

                if (JSON.stringify(entity) === JSON.stringify(team.persistObject.resourceName))
                    return false;
                return true;
            };

            team.updateObject = function (teamData) {

                if (!team.Ischanged(teamData)) return;

                var teamEntity = {
                    TeamId: teamData.teamId,
                    TeamName: teamData.teamName
                }
                
                var promise = adminService.UpdateTeam(teamEntity);

                promise.then(function (result) {
                    var index = _.findIndex(team.data, 'teamId', result.teamId);
                    team.data[index] = result;
                }).catch(function (error) {
                    toastr.error(error);
                });
            }

            team.deleteObject = function (teamData)
            {
                var promise = adminService.DeleteTeam(teamData.teamId);

                promise.then(function (result) {
                    var index = _.findIndex(team.data, 'teamId', result);
                    team.data.splice(index, 1);
                }).catch(function (error) {
                    toastr.error(error);
                });
            }

        });
})();