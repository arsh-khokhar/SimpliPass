import React from "react";
import Header from "./components/Header";
import Footer from "./components/Footer";
import Loader from "./components/Loader";
import DepartmentSelector from "./components/DepartmentSelector";
import CourseSelector from "./components/CourseSelector";
import CourseView from "./components/CourseView";
import Dialog from "./components/Dialog";
import { proxyURL, apiRootURL } from "./Utils/constants";

class App extends React.Component {
  constructor() {
    super();

    this.state = {
      currDept: "",
      currCourse: "",
      coursesList: [],
      loaded: false,
      serverError: false
    };
  }

  componentDidMount() {
    fetch(proxyURL + apiRootURL + "course/")
      .then(response => response.json())
      .then(result => {
        this.setState({ coursesList: result, loaded: true });
      })
      .catch(error => {
        this.setState({ serverError: true });
        console.error("Error:", error);
      });
  }

  handleSelectDept = dept => {
    this.setState({ currDept: dept, currCourse: "" });
  };

  handleSelectCourse = crs => {
    this.setState({ currCourse: crs });
  };

  renderBody() {
    return (
      <>
        <DepartmentSelector
          handleSelectDept={this.handleSelectDept}
          coursesList={this.state.coursesList}
        />
        <CourseSelector
          coursesList={this.state.coursesList}
          handleSelectCourse={this.handleSelectCourse}
          currDept={this.state.currDept}
        />
        <CourseView
          currCourse={this.state.currCourse}
          coursesList={this.state.coursesList}
        />
      </>
    );
  }

  renderServerError() {
    return (
      <Dialog
        type="error"
        heading="Server Error!"
        message="Please try refreshing."
      />
    );
  }

  render() {
    return (
      <div className="App">
        <Header />

        {this.state.serverError && this.renderServerError()}

        {this.state.loaded && !this.state.serverError ? (
          this.renderBody()
        ) : (
          <Loader />
        )}

        <Footer />
      </div>
    );
  }
}

export default App;
