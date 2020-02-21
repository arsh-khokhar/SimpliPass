import React from "react";
import Enzyme, { shallow } from "enzyme";
import Adapter from "enzyme-adapter-react-16";
import CourseView from "../components/CourseView";
import { sampleCoursesList } from "./testData";

Enzyme.configure({ adapter: new Adapter() });

describe("CourseView", () => {
  it("should render a course Header containing the course id, course name and department name and also course difficulty and its list of section ratings based on the props of coursesList and the current Course selected", () => {
    const component = shallow(
      <CourseView currCourse={"COMP 2140"} coursesList={sampleCoursesList} />
    );

    // Course ID and Name
    expect(component.find("h1")).toHaveLength(1);
    expect(component.find("h1").text()).toEqual(
      "COMP 2140 - Data Structures and Algorithms"
    );

    // Department Name
    expect(component.find("h2")).toHaveLength(1);
    expect(component.find("h2").text()).toEqual(
      "Department of Computer Science"
    );

    // Difficulty Level
    expect(component.find("span")).toHaveLength(1);
    expect(component.find("span").text()).toEqual("5");

    // Section Ratings
    expect(component.find("h5").text()).toEqual("Section Ratings");
    expect(component.find("p")).toHaveLength(3);

    expect(
      component
        .find("p")
        .at(0)
        .text()
    ).toEqual("Brad Pitt:  8 (2 reviews)");
    expect(
      component
        .find("p")
        .at(1)
        .text()
    ).toEqual("Tom Hanks:  6 (3 reviews)");

    expect(
      component
        .find("p")
        .at(2)
        .text()
    ).toEqual("Robert Downey, Jr.:  9 (5 reviews)");
  });
});
