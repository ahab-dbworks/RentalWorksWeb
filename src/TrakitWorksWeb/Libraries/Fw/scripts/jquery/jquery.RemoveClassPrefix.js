// Remove classes that have given prefix
// Example: 
// You have an element with classes "apple juiceSmall juiceBig banana"
// You run:
// $elem.removeClassPrefix('juice');
// The resulting classes are "apple banana"
 
// NOTE: discussion of implementation techniques for this, including why simple RegExp with word boundaries isn't correct: 
// http://stackoverflow.com/questions/57812/jquery-remove-all-classes-that-begin-with-a-certain-string#comment14232343_58533
 
jQuery.fn.removeClassPrefix = function (prefix) {
    this.each( function ( i, it ) {
        var classes = it.className.split(" ").map(function (item) {
            return item.indexOf(prefix) === 0 ? "" : item;
        });
        classes = classes.filter(function(n){return n}); //Removes empty elements
        it.className = classes.join(" ");
    });
    return this;
};
