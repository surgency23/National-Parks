SELECT site.site_id, site.max_occupancy, site.accessible, site.utilities, campground.name FROM site
JOIN campground ON campground.campground_id = site.campground_id
JOIN reservation ON reservation.site_id = site.site_id
WHERE site.site_id not in (SELECT reservation.site_id FROM reservation
WHERE reservation.from_date between '2018-02-19' AND '2018-02-25' AND reservation.to_date between '2018-02-19' AND '2018-02-25')

(SELECT reservation.site_id, reservation.reservation_id FROM reservation
WHERE reservation.from_date between '2018-02-19' AND '2018-02-25' AND reservation.to_date between '2018-02-19' AND '2018-02-25')

SELECT reservation.site_id, reservation.name, reservation.from_date, reservation.to_date FROM reservation
WHERE From_date > '2018-02-19' AND To_date < '2018-02-25'

SELECT site.site_id, site.max_occupancy, site.accessible, site.utilities, campground.name, reservation.reservation_id, reservation.name, reservation.from_date, reservation.to_date FROM site
JOIN campground ON campground.campground_id = site.campground_id
JOIN reservation ON reservation.site_id = site.site_id
WHERE site.site_id in (SELECT reservation.site_id FROM reservation
WHERE reservation.from_date between '2018-02-21' AND '2018-02-23' AND reservation.to_date between '2018-02-21' AND '2018-02-23'
 OR (reservation.from_date <= '2018-02-21' AND reservation.to_date >= '2018-02-23'))
-- OR (reservation.from_date >= '2018-02-21' AND reservation.to_date <= '2018-02-23')

SELECT site.site_id, site.max_occupancy, site.accessible, site.utilities, campground.name FROM site
JOIN campground ON campground.campground_id = site.campground_id
JOIN reservation ON reservation.site_id = site.site_id
WHERE site.site_id not in (SELECT reservation.site_id FROM reservation
WHERE reservation.from_date between '2018-02-19' AND '2018-02-25' AND reservation.to_date between '2018-02-19' AND '2018-02-25')

SELECT *, campground.name, site.site_id FROM reservation
JOIN site ON site.site_id = reservation.site_id
JOIN campground ON campground.campground_id = site.campground_id ORDER BY reservation.from_date, campground.name