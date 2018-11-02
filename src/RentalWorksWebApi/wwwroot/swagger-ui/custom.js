let header = [];
header.push('<div>');
header.push('  <div>There are certain parts about the Api that can not be fully documented using swagger.  One of those is that we use filter expressions as part of GET requests.  This allows a compact and powerful notation for performing filtering against a resource.  Whenever a field in Swagger says filter expression.  Please refer to the documentation below for constructing a filter expression.</div>');
header.push('  <div>Filter Expression: {value} or {eq|ne|in|ni|sw|ew|co|dnc|lt|lte|gt|gte}:{values}</div>');
header.push('  <ul>');
header.push('    <li>eq: equals</li>');
header.push('    <li>ne: not equals</li>');
header.push('    <li>in: in</li>');
header.push('    <li>ni: not in</li>');
header.push('    <li>sw: start with</li>');
header.push('    <li>ew: ends with</li>');
header.push('    <li>co: contains</li>');
header.push('    <li>dnc: does not contain</li>');
header.push('    <li>lt: less than</li>');
header.push('    <li>lte: less than or equal to</li>');
header.push('    <li>gt: greater than</li>');
header.push('    <li>gte: greater than or equal to</li>');
header.push('  </ul>');
header.push('</div>');
header = header.join('\n');


let el_description_container = document.querySelector('.info-container .info .description .markdown');
el_description_container.insertAdjacentHTML('beforeend', header);