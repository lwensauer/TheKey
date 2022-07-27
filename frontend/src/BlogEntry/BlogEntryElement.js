import React from 'react';

const BlogEntryElement = (props) => (
    <div style={{ background: "#eee", borderRadius: '5px', padding: '0 10px' }}>
        <p><strong>{props.id} - {props.title} </strong></p>
        <p>{props.message}</p>
{/*
  -- Liste anzeigen? Nicht gefordert, nur senden an Frontend
   {Object.entries(props.wordCounterMap).map(([key, value]) => (
       <p>{key}={value}</p>
           ))}
    */}

  </div>
);

export default BlogEntryElement;