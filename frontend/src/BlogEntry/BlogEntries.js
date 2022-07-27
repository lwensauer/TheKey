import React, { useState, useEffect, useRef } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';

import BlogEntriesView from './BlogEntriesView';

const BlogEntries = () => {
    const [ blog, setBlog ] = useState([]);
    const latestBlog = useRef(null);

    latestBlog.current = blog;

    useEffect(() => {
        const connection = new HubConnectionBuilder()
            .withUrl('https://localhost:5001/hubs/blog')
            .withAutomaticReconnect()
            .build();

        connection.start()
            .then(result => {
                console.log('Connection-ID: ' + connection.connectionId);
                
                connection.on('NewBlogEntry', message => {
                    const newBlog = [...latestBlog.current];
                    newBlog.push(message);
                
                    setBlog(newBlog);
                });
            })
            .catch(e => console.log('Connection failed: ', e));
    }, []);

    return (
        <div>
            <BlogEntriesView blog={blog}/>
        </div>
    );
};

export default BlogEntries;
