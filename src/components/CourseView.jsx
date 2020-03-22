import React from "react";
import "../styles/courseView.css";
import { getCourseInfo } from "../Utils/utils";
import Badge from "react-bootstrap/Badge";
import ReactTooltip from "react-tooltip";
import { Container, Row, Col } from "react-bootstrap";

const CourseView = props => {
  const courseInfo = getCourseInfo(props.currCourse, props.coursesList);

  const renderCourseHeader = () => {
    return (
      <Col className="header">
        <h1 id="courseName">
          {courseInfo.id} - {courseInfo.name}
        </h1>
        <h2 id="departmentName">Department of {courseInfo.department}</h2>
      </Col>
    );
  };

  const renderDifficulty = () => {
    let modifier;

    if (courseInfo.difficulty >= 8) {
      modifier = "danger";
    } else if (courseInfo.difficulty >= 5 && courseInfo.difficulty < 8) {
      modifier = "warning";
    } else if (courseInfo.difficulty >= 0 && courseInfo.difficulty < 5) {
      modifier = "success";
    }

    return (
      <Col className="difficultyContainer">
        <h6 data-tip="Out of 10. Lower number indicates easier course.">
          Difficulty Level
          <span className="difficultyNumber">
            <Badge variant={modifier}>{courseInfo.difficulty}</Badge>
          </span>
        </h6>
        <div className="difficultyCount">
          Based on
          {courseInfo.difficultyCount > 1
            ? ` ${courseInfo.difficultyCount} reviews`
            : ` ${courseInfo.difficultyCount} review`}
        </div>
      </Col>
    );
  };

  const renderSectionRatings = () => {
    let sectionRatingsList = [];

    for (let [key, value] of Object.entries(courseInfo.sectionRatings)) {
      sectionRatingsList.push(
        <div className="sectionName" key={key}>
          {`${key}:  ${value.rating} `}
          <p>{` ${value.count} reviews`}</p>
        </div>
      );
    }

    return (
      <Col className="sectionRatingsContainer">
        <h5
          className="sectionRatingsHeader"
          data-tip="Out of 10. Higher number indicates better instructor."
        >
          Section Ratings
        </h5>

        <div className="sectionList">{sectionRatingsList}</div>
      </Col>
    );
  };

  return (
    courseInfo.length !== 0 && (
      <Container className="courseViewContainer">
        <ReactTooltip place="top" effect="solid" />
        <Row>{renderCourseHeader()}</Row>
        <Row>{renderDifficulty()}</Row>
        <Row>{renderSectionRatings()}</Row>
      </Container>
    )
  );
};

export default CourseView;
