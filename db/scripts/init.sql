CREATE EXTENSION IF NOT EXISTS postgis;
CREATE EXTENSION IF NOT EXISTS postgis_topology;

CREATE TABLE users
(
    id SERIAL,
    name text,
    location geography
);

INSERT INTO users
    (name, location)
values
    ('John The Pastor', ST_GeographyFromText('POINT(55.019637 82.922654)')),
    ('Peter The Minister', ST_GeographyFromText('POINT(55.019054 82.927947)')),
    ('Sara The Doctor', ST_GeographyFromText('POINT(55.017255 82.938452)')),
    ('Vasya The Student', ST_GeographyFromText('POINT(55.016911 82.938366)'));



CREATE OR REPLACE FUNCTION search_intersected(source_user_id integer, search_radius integer)

RETURNS SETOF users AS
$BODY$
BEGIN
    RETURN QUERY
    SELECT *
    FROM users
    WHERE ST_Intersects(st_buffer(location, search_radius), 
                            (select st_buffer(location, search_radius)
        from users
        where id = source_user_id))
        AND id <> source_user_id;
END
$BODY$ 
LANGUAGE plpgsql;