angular.module('my-element',[])
    .directive('myTextInput', function() {
        return {
            template: '<input type="text" maxlength="{{::getLength()}}" placeholder="{{::getPlaceHolder()}}" aria-label="{{::getPlaceHolder()}}" />',
            restrict: 'AE',
            replace: true,
            scope: {
                config: '=',
                isNew: '=?'
            },
            link:function postLink(scope, element, attr) {

                scope.getLength = function() {
                    if (!scope.config) {
                        return -1;
                    }

                    return scope.config.FieldLength;
                };

                scope.getPlaceHolder = function() {

                    return scope.config.Label;
                };
            }
        };
    });