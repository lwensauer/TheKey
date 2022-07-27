import React from 'react';

import BlogEntryElement from './BlogEntryElement';

const BlogEntriesView = (props) => {
    const blogEntry = props.blog
        .map(m => <BlogEntryElement
            key={m.id}
            id={m.id}
            title={m.title}
            message={m.message}
            wordCounterMap={m.wordCounterMap}
        />);
    return (
        <div>
            {blogEntry}
        </div>
    )
};

export default BlogEntriesView;