"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
var _this = this;
Object.defineProperty(exports, "__esModule", { value: true });
require('dotenv').config();
var ModuleBase_1 = require("../shared/ModuleBase");
var Vendor_1 = require("./modules/Vendor");
var _a = require('winston'), createLogger = _a.createLogger, format = _a.format, transports = _a.transports;
var combine = format.combine, timestamp = format.timestamp, label = format.label, printf = format.printf;
var myFormat = printf(function (_a) {
    var level = _a.level, message = _a.message, label = _a.label, timestamp = _a.timestamp;
    return timestamp + " " + level + ": " + message;
});
var logger = createLogger({
    format: combine(timestamp(), myFormat),
    defaultMeta: { service: 'user-service' },
    transports: [
        new transports.Console(),
        new transports.File({ filename: 'error.log', level: 'error' }),
        new transports.File({ filename: 'combined.log', level: 'info' }),
    ]
});
try {
    if (process.env.RW_EMAIL === undefined)
        throw 'Please add a line to the .env file such as RW_EMAIL=\'TEST\'';
    if (process.env.RW_EMAIL === undefined)
        throw 'Please add a line to the .env file such as RW_EMAIL=\'TEST\'';
    //globals
    var continueTest_1 = true;
    //login
    test('Login', function () { return __awaiter(_this, void 0, void 0, function () {
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0: return [4 /*yield*/, ModuleBase_1.ModuleBase.authenticate()
                        .then(function (data) { })
                        .catch(function (err) { return logger.error('authenticate: ', err); })];
                case 1:
                    continueTest_1 = _a.sent();
                    return [2 /*return*/];
            }
        });
    }); }, 45000);
    ModuleBase_1.ModuleBase.emailResults();
    //vendor
    if (continueTest_1) {
        var module_1 = new Vendor_1.Vendor();
        describe('Create new Vendor, fill out form, save record', function () {
            if (continueTest_1) {
                test('Open module and create', function () { return __awaiter(_this, void 0, void 0, function () {
                    return __generator(this, function (_a) {
                        switch (_a.label) {
                            case 0: return [4 /*yield*/, module_1.openModule()
                                    .then()
                                    .catch(function (err) { return logger.error('openModule: ', err); })];
                            case 1:
                                _a.sent();
                                return [2 /*return*/];
                        }
                    });
                }); }, 10000);
                test('Create New record', function () { return __awaiter(_this, void 0, void 0, function () {
                    return __generator(this, function (_a) {
                        switch (_a.label) {
                            case 0: return [4 /*yield*/, module_1.createNewRecord(1)
                                    .then()
                                    .catch(function (err) { return logger.error('createNewRecord: ', err); })];
                            case 1:
                                _a.sent();
                                return [2 /*return*/];
                        }
                    });
                }); }, 10000);
                test('Fill in Vendor form data', function () { return __awaiter(_this, void 0, void 0, function () {
                    return __generator(this, function (_a) {
                        switch (_a.label) {
                            case 0: return [4 /*yield*/, module_1.populateNewVendor()
                                    .then()
                                    .catch(function (err) { return logger.error('populateNewVendor: ', err); })];
                            case 1:
                                _a.sent();
                                return [2 /*return*/];
                        }
                    });
                }); }, 10000);
                test('Save new Vendor', function () { return __awaiter(_this, void 0, void 0, function () {
                    return __generator(this, function (_a) {
                        switch (_a.label) {
                            case 0: return [4 /*yield*/, module_1.saveRecord()
                                    .then()
                                    .catch(function (err) { return logger.error('saveRecord: ', err); })];
                            case 1:
                                _a.sent();
                                return [2 /*return*/];
                        }
                    });
                }); }, 10000);
                test('Close Record', function () { return __awaiter(_this, void 0, void 0, function () {
                    return __generator(this, function (_a) {
                        switch (_a.label) {
                            case 0: return [4 /*yield*/, module_1.closeRecord()
                                    .then()
                                    .catch(function (err) { return logger.error('closeRecord: ', err); })];
                            case 1:
                                _a.sent();
                                return [2 /*return*/];
                        }
                    });
                }); }, 10000);
            }
        });
        describe('Create new Vendor, fill out form without a required field and attempt to save record', function () {
            test('Open module and create', function () { return __awaiter(_this, void 0, void 0, function () {
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0: return [4 /*yield*/, module_1.openModule()
                                .then()
                                .catch(function (err) { return logger.error('openModule: ', err); })];
                        case 1:
                            _a.sent();
                            return [2 /*return*/];
                    }
                });
            }); }, 10000);
            test('Create New record', function () { return __awaiter(_this, void 0, void 0, function () {
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0: return [4 /*yield*/, module_1.createNewRecord(1)
                                .then()
                                .catch(function (err) { return logger.error('createNewRecord: ', err); })];
                        case 1:
                            _a.sent();
                            return [2 /*return*/];
                    }
                });
            }); }, 10000);
            test('Fill in Vendor form data without Vendor', function () { return __awaiter(_this, void 0, void 0, function () {
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0: return [4 /*yield*/, module_1.populateNewVendorWithoutVendor()
                                .then()
                                .catch(function (err) { return logger.error('populateNewVendor: ', err); })];
                        case 1:
                            _a.sent();
                            return [2 /*return*/];
                    }
                });
            }); }, 10000);
            test('Save new Vendor', function () { return __awaiter(_this, void 0, void 0, function () {
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0: return [4 /*yield*/, module_1.saveRecord()
                                .then()
                                .catch(function (err) { return logger.error('saveRecord: ', err); })];
                        case 1:
                            continueTest_1 = _a.sent();
                            return [2 /*return*/];
                    }
                });
            }); }, 10000);
            console.log('continueTest? ', continueTest_1); // leaving this for display purposes only. as you'll see in console, this is logged before any tests are executed. i guess the test suite runs through all the tests prior and builds a list. therefore a flag like this alone wont keep tests from being run.
            test('Close Record', function () { return __awaiter(_this, void 0, void 0, function () {
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0: return [4 /*yield*/, module_1.closeRecord()
                                .then()
                                .catch(function (err) { return logger.error('closeRecord: ', err); })];
                        case 1:
                            _a.sent();
                            return [2 /*return*/];
                    }
                });
            }); }, 10000);
        });
    }
    //logoff
    test('Logoff', function () { return __awaiter(_this, void 0, void 0, function () {
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0: return [4 /*yield*/, ModuleBase_1.ModuleBase.logoff()
                        .then(function (data) { })
                        .catch(function (err) { return logger.error('logoff: ', err); })];
                case 1:
                    continueTest_1 = _a.sent();
                    return [2 /*return*/];
            }
        });
    }); }, 45000);
}
catch (ex) {
    logger.error('Error in catch LoginCreateNewVendor', ex);
}
//# sourceMappingURL=NewVendor.test.js.map