# Changelog

All notable changes to this project will be documented in this file. See [standard-version](https://github.com/conventional-changelog/standard-version) for commit guidelines.

## [3.4.0](https://github.com/derekgreer/expectedObjects/compare/v3.3.1...v3.4.0) (2021-01-11)


### Features

* Add ability to ignore use of Equals() override ([1a2f268](https://github.com/derekgreer/expectedObjects/commit/1a2f2683eb26799b3f2fd01d79e9fd1798c0a9a1))


### Bug Fixes

* Correct configuration to include public fields ([8de6649](https://github.com/derekgreer/expectedObjects/commit/8de6649f2318c7098aa990c0a7bb92e9bd77e2e4))
* Modify strategy for classes to require members ([f1c4e7c](https://github.com/derekgreer/expectedObjects/commit/f1c4e7c5697a5773388a6045d507d0b76c052072))

### [3.3.1](https://github.com/derekgreer/expectedObjects/compare/v3.3.0...v3.3.1) (2021-01-05)


### Bug Fixes

* Correct ignore for different types ([4d4274b](https://github.com/derekgreer/expectedObjects/commit/4d4274bf24c77b9682c54a601e5ae5f611c238b0))

## [3.3.0](https://github.com/derekgreer/expectedObjects/compare/v3.2.1...v3.3.0) (2021-01-05)


### Features

* Add explicit relative member path support ([88bb5c2](https://github.com/derekgreer/expectedObjects/commit/88bb5c288d5596e478934fa84e334e6992912257))

### [3.2.1](https://github.com/derekgreer/expectedObjects/compare/v3.2.0...v3.2.1) (2021-01-02)


### Bug Fixes

* Correct relative path application ([b400cf1](https://github.com/derekgreer/expectedObjects/commit/b400cf1117f55938d7831fdbd68db8036d93c1a6))

## [3.2.0](https://github.com/derekgreer/expectedObjects/compare/v3.1.0...v3.2.0) (2021-01-01)


### Features

* Add ability to ignore by path ([#35](https://github.com/derekgreer/expectedObjects/issues/35)) ([e1d7312](https://github.com/derekgreer/expectedObjects/commit/e1d73120111d8d258ad29d3fff03b7be14a996c6)), closes [#24](https://github.com/derekgreer/expectedObjects/issues/24)

## [3.1.0](https://github.com/derekgreer/expectedObjects/compare/v3.0.0...v3.1.0) (2020-12-31)


### Features

* Allow chaining of ignored members ([d7f21d0](https://github.com/derekgreer/expectedObjects/commit/d7f21d03bad081f75308e450f0628467bc5324b3))

## [3.0.0](https://github.com/derekgreer/expectedObjects/compare/v2.3.5...v3.0.0) (2020-12-29)


### ⚠ BREAKING CHANGES

* This feature modifies the IComparisonStrategy to pass
in the expected and actual objects. This allows the strategy to evaluate
both objects qualify for the comparision operation.

### Features

* Correct issue with comparision of unlike types ([e49f01c](https://github.com/derekgreer/expectedObjects/commit/e49f01ca53a9776c94cd5a418cae3c6bac27a2d1))


### Bug Fixes

* Correct errors for overridden/hidden properties ([289c1a8](https://github.com/derekgreer/expectedObjects/commit/289c1a850446d0c6e77546a9f1eeb18d50f78868))

### [2.3.6](https://github.com/derekgreer/expectedObjects/compare/v2.3.5...v2.3.6) (2020-11-12)


### Bug Fixes

* Correct errors for overridden/hidden properties ([0190dc8](https://github.com/derekgreer/expectedObjects/commit/0190dc8b309636835230a4ae716390037ec34373))

### [2.3.5](https://github.com/derekgreer/expectedObjects/compare/v2.3.3...v2.3.5) (2020-09-02)


### Bug Fixes

* Correct formatting issue for booleans and certain numeric types ([23d9e73](https://github.com/derekgreer/expectedObjects/commit/23d9e739ed47fdfbb630893961c5fdbcdc3206e6))
* Correct issue where DateTimeOffset type causes StackOverflowException ([94594f9](https://github.com/derekgreer/expectedObjects/commit/94594f9899ad4f90db110d8ea1470101326840ef))

<a name="2.3.4"></a>
## [2.3.4](https://github.com/derekgreer/expectedObjects/compare/v2.3.3...v2.3.4) (2017-11-26)


### Bug Fixes

* Correct formatting issue for booleans and certain numeric types ([23d9e73](https://github.com/derekgreer/expectedObjects/commit/23d9e73))



<a name="2.3.3"></a>
## [2.3.3](https://github.com/derekgreer/expectedObjects/compare/v2.3.2...v2.3.3) (2017-10-25)


### Bug Fixes

* Correct issue with custom comparisons and missing elements where comparison was always true. ([9a59a39](https://github.com/derekgreer/expectedObjects/commit/9a59a39))



<a name="2.3.2"></a>
## [2.3.2](https://github.com/derekgreer/expectedObjects/compare/v2.3.1...v2.3.2) (2017-10-02)


### Bug Fixes

* Correct exception when comparing types with overloaded indexes ([422bdf1](https://github.com/derekgreer/expectedObjects/commit/422bdf1))



<a name="2.3.1"></a>
## [2.3.1](https://github.com/derekgreer/expectedObjects/compare/v2.3.0...v2.3.1) (2017-09-24)


### Bug Fixes

* Correct reporting issue when null object is displayed. ([8178f7c](https://github.com/derekgreer/expectedObjects/commit/8178f7c))
* Correct reporting issue with recursive instances. ([4702d50](https://github.com/derekgreer/expectedObjects/commit/4702d50))



<a name="2.3.0"></a>
# [2.3.0](https://github.com/derekgreer/expectedObjects/compare/v2.2.0...v2.3.0) (2017-09-22)


### Bug Fixes

* Fix parallel test output ([a3ed69e](https://github.com/derekgreer/expectedObjects/commit/a3ed69e))


### Features

* Added Expect.Default<T>() and Expect.NotDefault<T>() custom comparisons. ([2a5b1bc](https://github.com/derekgreer/expectedObjects/commit/2a5b1bc))



<a name="2.2.0"></a>
# [2.2.0](https://github.com/derekgreer/expectedObjects/compare/v2.1.1...v2.2.0) (2017-09-13)


### Features

* Add ability to configure custom comparsions for specific members. ([abab048](https://github.com/derekgreer/expectedObjects/commit/abab048))



<a name="2.1.1"></a>
## [2.1.1](https://github.com/derekgreer/expectedObjects/compare/v2.1.0...v2.1.1) (2017-09-04)


### Bug Fixes

* typo corrections ([e978c39](https://github.com/derekgreer/expectedObjects/commit/e978c39))



<a name="2.1.0"></a>
# [2.1.0](https://github.com/derekgreer/expectedObjects/compare/v2.0.0...v2.1.0) (2017-08-29)


### Features

* Add configuration to enable ordinal comparisons. ([2b26e25](https://github.com/derekgreer/expectedObjects/commit/2b26e25))



<a name="2.0.0"></a>
# [2.0.0](https://github.com/derekgreer/expectedObjects/compare/v1.3.1...v2.0.0) (2017-08-29)


### Features

* Redesign test output to display approximation of expected and actual values ([1454dc2](https://github.com/derekgreer/expectedObjects/commit/1454dc2))


### BREAKING CHANGES

* Add ability to ignore the order when comparing instances of IEnumerable ([1454dc2](https://github.com/derekgreer/expectedObjects/commit/1454dc2))
* Replace ExpectedObject.Configure() method with ExpectedObjectBuilder ([1454dc2](https://github.com/derekgreer/expectedObjects/commit/1454dc2))
* Replace the IgnoreTypes() method with ExpectedObject extension methods: Matches(), DoesNotMatch() ([1454dc2](https://github.com/derekgreer/expectedObjects/commit/1454dc2))


### Chores

* chore(build): Add build support for packing and publishing pre-releases ([1454dc2](https://github.com/derekgreer/expectedObjects/commit/1454dc2))



<a name="1.3.1"></a>
## [1.3.1](https://github.com/derekgreer/expectedObjects/compare/v1.3.0...v1.3.1) (2017-08-26)


### Bug Fixes

* Prevent infinite recursive comparisions ([837ea19](https://github.com/derekgreer/expectedObjects/commit/837ea19))



<a name="1.3.0"></a>
# 1.3.0 (2017-05-09)


### Features

* Added support for .Net Standard 1.3 ([60e65e3](https://github.com/derekgreer/expectedObjects/commit/60e65e3))
